import classes from "./client-table.module.scss";
import {useEffect, useState} from "react";
import type {IClient} from "./Client.ts";
import {useAuth} from "../../Contexts/AuthContext.tsx";

const ClientTable = () => {
    const [clients, setClients] = useState<IClient[]>([]);
    const auth = useAuth();

    useEffect(() => {
        fetch("http://localhost:5000/clients", {
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
            .then(r => setClients(r));
    }, [])

    return (
        <table className={classes.table}>
            <thead className={classes.thead}>
                <tr>
                    <th>Client ID</th>
                    <th>Client Name</th>
                    <th>Contact Email</th>
                    <th>Balance</th>
                </tr>
            </thead>

            <tbody className={classes.tbody}>
            {clients.map((client) => (
                <tr className={classes.tr} key={client.clientId}>
                    <td className={classes.td}>{client.clientId.slice(0, client.clientId.indexOf("-")) + "..."}</td>
                    <td className={classes.td}>{client.name}</td>
                    <td className={classes.td}>{client.email}</td>
                    <td className={classes.td}>{client.balanceT.toFixed(2)}</td>
                </tr>
            ))}
            </tbody>
        </table>
    );
};

export default ClientTable;