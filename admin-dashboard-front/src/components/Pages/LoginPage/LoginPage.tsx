import LoginForm from "../../UI/LoginForm/LoginForm.tsx";
import classes from "./LoginPage.module.css";

const LoginPage = () => {
    return (
        <div className={classes.content}>
            <LoginForm/>
        </div>
    );
};

export default LoginPage;