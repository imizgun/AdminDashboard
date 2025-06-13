import {useState} from "react";
import classes from "./loginform.module.scss";

const LoginForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    return (
        <div className={classes.content}>
            <div style={{marginBottom: 10}}>Login as Admin</div>
            <form onSubmit={(e) => e.preventDefault()}>
                <div>
                    <input
                        className="input"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        placeholder="Email"
                        type="text"
                        name="email" required />
                </div>
                <div>
                    <input
                        className="input"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        type="password"
                        placeholder="Password"
                        name="password" required />
                </div>
                <button className="button" type="submit" onSubmit={() => console.log(email, password)}>Login</button>
            </form>
            <div>
                {email}
                {password}
            </div>
        </div>
    );
};

export default LoginForm;