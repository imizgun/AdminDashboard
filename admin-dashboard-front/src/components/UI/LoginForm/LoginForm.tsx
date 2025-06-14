import {useEffect, useState} from "react";
import classes from "./loginform.module.scss";
import {useNavigate} from "react-router-dom";
import {useAuth} from "../../Contexts/AuthContext.tsx";

const LoginForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [errorActive, setErrorActive] = useState(false);
    const navigate = useNavigate();
    const auth = useAuth();

    useEffect(() => {
        if (localStorage.getItem('token')) {
            navigate("/dashboard");
        }
    }, [])

    return (
        <div className={classes.content}>
            <div style={{marginBottom: 10}}>Login as Admin</div>
            <form onSubmit={(e) => e.preventDefault()}>
                <div>
                    <input
                        className="input"
                        value={email}
                        onChange={(e) => {
                            setEmail(e.target.value)
                            setErrorActive(false);
                        }}
                        placeholder="Email"
                        type="text"
                        name="email" required />
                </div>
                <div>
                    <input
                        className="input"
                        value={password}
                        onChange={(e) => {
                            setPassword(e.target.value)
                            setErrorActive(false)
                            }
                        }
                        type="password"
                        placeholder="Password"
                        name="password" required />
                </div>
                <button
                    className="button"
                    type="submit"
                    onClick={
                    () => fetch("http://localhost:5000/auth/login", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify({email, password})
                    })
                        .then(res => res.json())
                        .then(data => {
                            if (data.token) {
                                auth.setAuth({
                                    token: data.token,
                                    email: email
                                });
                                navigate("/dashboard");
                            } else {
                                setErrorActive(true);
                                setError(data.message || "Login failed");
                            }
                        })
                        .catch(err => {
                            setErrorActive(true);
                            setError("An error occurred while logging in");
                            console.error(err);
                        })
                }>
                    Login
                </button>

                <div className={`${classes.error} ${errorActive ? classes.active : ""}`}>
                    {error}
                </div>
            </form>
        </div>
    );
};

export default LoginForm;