import LoginForm from "../../UI/LoginForm/LoginForm.tsx";
import classes from "./loginpage.module.scss";

const LoginPage = () => {
    return (
        <div className={classes.content}>
            <LoginForm/>
        </div>
    );
};

export default LoginPage;