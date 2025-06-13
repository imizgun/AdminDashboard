import {Outlet} from "react-router-dom";
import Header from "../../UI/Header/Header.tsx";
import classes from "./layout.module.scss";

const Layout = () => {
    return (
        <div className={classes.layout}>
            <div className={classes.header}>
                <Header />
            </div>
            <div className={classes.content}>
                <Outlet/>
            </div>
        </div>
    );
};

export default Layout;