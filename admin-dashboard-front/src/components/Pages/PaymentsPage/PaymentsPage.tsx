import PaymentTable from "../../UI/PaymentTable/PaymentTable.tsx";
import {useNavigate} from "react-router-dom";
import {useEffect} from "react";
import {useAuth} from "../../Contexts/AuthContext.tsx";


const PaymentsPage = () => {
    const navigate = useNavigate();
    const auth = useAuth()

    useEffect(() => {
        if (!localStorage.getItem('token')) {
            navigate("/login");
        }
    }, [auth])

    return (
        <div style={{margin: '20px'}}>
            <PaymentTable />
        </div>
    );
};

export default PaymentsPage;