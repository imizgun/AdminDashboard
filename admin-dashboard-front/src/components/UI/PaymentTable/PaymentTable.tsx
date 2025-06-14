import {useEffect, useState} from 'react';
import classes from "../ClientTable/client-table.module.scss";
import type {IPayment} from "./IPayment.ts";
import {useAuth} from "../../Contexts/AuthContext.tsx";

const PaymentTable = () => {
    const [payments, setPayments] = useState<IPayment[]>([]);
    const [skip, setSkip] = useState(0);
    const [take] = useState(2);
    const auth = useAuth();

    useEffect(() => {
        fetch(`http://localhost:5000/payments?take=${take}&skip=${skip * take}`, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        })
            .then(res => {
                if (res.status === 401) {
                    auth.logout()
                    throw new Error('Failed to fetch payments');
                }
                return res.json()
            })
            .then(r => setPayments([...payments, ...r]));
    }, [skip, auth])

    return (
        <>
            <table className={classes.table}>
            <thead className={classes.thead}>
            <tr>
                <th>Payment ID</th>
                <th>Sender Email</th>
                <th>Receiver Email</th>
                <th>Amount</th>
                <th>Date</th>
            </tr>
            </thead>

            <tbody className={classes.tbody}>
            {payments.map((payment) => (
                <tr className={classes.tr} key={payment.paymentId}>
                    <td className={classes.td}>{payment.paymentId.slice(0, payment.paymentId.indexOf("-")) + "..."}</td>
                    <td className={classes.td}>{payment.senderEmail}</td>
                    <td className={classes.td}>{payment.receiverEmail}</td>
                    <td className={classes.td}>{payment.amount.toFixed(2)}</td>
                    <td className={classes.td}>{payment.paymentDate}</td>
                </tr>
            ))}
            </tbody>
        </table>
            <div>
                <button style={{marginTop: 20}} className="button" onClick={() => {
                    setSkip(skip + 1);
                }}>
                    Load More
                </button>
            </div>
        </>

    );
};

export default PaymentTable;