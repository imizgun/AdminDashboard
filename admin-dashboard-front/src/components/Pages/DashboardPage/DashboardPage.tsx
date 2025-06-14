import ClientTable from "../../UI/ClientTable/ClientTable.tsx";
import RateBlock from "../../UI/RateBlock/RateBlock.tsx";
import classes from "./dashboard-page.module.scss";
import {useNavigate} from "react-router-dom";
import {useEffect} from "react";
import {useAuth} from "../../Contexts/AuthContext.tsx";

const DashboardPage = () => {
    const navigate = useNavigate();
    const auth = useAuth()

    useEffect(() => {
        if (!localStorage.getItem('token')) {
            navigate("/login");
        }
    }, [auth])

    return (
        <div className={classes.dashboard}>
            <div className={classes.table}>
                <ClientTable/>
            </div>
            <RateBlock/>
        </div>
    );
};

export default DashboardPage;