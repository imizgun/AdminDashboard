import classes from "./rate-block.module.scss"
import {useEffect, useState} from "react";
import {useAuth} from "../../Contexts/AuthContext.tsx";

const RateBlock = () => {
    const [rate, setRate] = useState<number>(10);
    const [newRate, setNewRate] = useState<string>(rate.toString());
    const [isChanging, setIsChanging] = useState<boolean>(false);
    const [isError, setIsError] = useState<boolean>(false);
    const [message, setMessage] = useState<string>("");
    const auth = useAuth();

    useEffect(() => {
        fetch("http://localhost:5000/rate", {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        })
            .then(res =>{
                if (res.status === 401) {
                    auth.logout()
                    throw new Error('Failed to fetch payments');
                }
                return res.json()
            })
            .then(r => {
                setRate(r['rate']);
                setNewRate(r['rate'].toString());
            })
            .catch(err => {
                console.error("Failed to fetch rate:", err);
                setMessage("Failed to fetch rate");
            });
    }, [auth])

    return (
        <div className={classes.rate_block}>
            <div className={classes.title}>
                Current Rate
            </div>

            <input
                 value={isChanging ? newRate : rate}
                 disabled={!isChanging}
                 onChange={
                 (e) => setNewRate(e.target.value)}
                 className={isChanging ? classes.rate + " " + classes.active : classes.rate}>
            </input>

            <button className="button"
                    onClick={isChanging
                        ? () => {
                        const parsedRate = parseFloat(newRate);

                        if (isNaN(parsedRate) || parsedRate <= 0) {
                            setIsError(true);
                            return;
                        }
                        setNewRate(parsedRate.toString());

                        fetch("http://localhost:5000/rate", {
                            method: "POST",
                            headers: {
                                Authorization: `Bearer ${localStorage.getItem('token')}`,
                                "Content-Type": "application/json"
                            },
                            body: JSON.stringify({rate: parsedRate})
                        }).then(res => {
                            if (res.status === 401) {
                                auth.logout()
                                throw new Error('Failed to fetch payments');
                            }
                            if (res.status === 200)
                                setRate(parsedRate)
                            return res.json()
                        }).then(r => setMessage(r['message']));

                        setRate(parsedRate);
                        setIsChanging(false)}
                        : () => setIsChanging(true)
            }>
                {isChanging ? "Confirm" : "Change rate"}
            </button>


            <button
                className={isChanging ? `${classes.cancel_button} ${classes.active}` : classes.cancel_button}
                onClick={() => {
                    setIsChanging(false);
                    setIsError(false);
                    setNewRate(rate.toString());
                }
            }>
                Cancel changing
            </button>
            {isError && isChanging && <div style={{color: 'red'}} className={classes.error}>Please enter a valid rate</div>}
            {message && <div>{message}</div>}
        </div>
    );
};

export default RateBlock;