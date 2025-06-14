import { useAuth } from '../../Contexts/AuthContext';
import classes from './header.module.scss';
import {useEffect, useState} from "react";
import {Link} from "react-router-dom";

const Header = () => {
    const auth = useAuth();
    const [email, setEmail] = useState(auth.auth.email);

    useEffect(() => {
        setEmail(auth.auth.email);
    }, [auth.auth.email]);

    return (
        <header className={classes.header}>
            <div className={classes.right}>
                <div className={classes.title}>
                    Admin Dashboard
                </div>
                <div className={classes.nav}>
                    {
                        localStorage.getItem('token')
                            ? <Link to="/dashboard" className={classes.link}>Clients</Link>
                            : ""
                    }
                </div>
                <div className={classes.nav}>
                    {
                        localStorage.getItem('token')
                            ? <Link to="/payments" className={classes.link}>Payments</Link>
                            : ""
                    }
                </div>
            </div>
            <div className={classes.left}>
                <div>
                    {
                        localStorage.getItem('token')
                        ? <span>Logged in as <span style={{fontWeight: 'bold'}}>{email}</span></span>
                        : ""
                    }
                </div>
                <div className={classes.cross} onClick={() => auth.logout()}>
                    {
                        localStorage.getItem('token')
                        ? <img alt="log out cross" src="cross.svg" width={15} height={15} />
                        : ""
                    }

                </div>
            </div>
        </header>
    );
};

export default Header;