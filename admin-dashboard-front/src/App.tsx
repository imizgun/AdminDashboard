import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import Layout from "./components/Pages/Layout/Layout.tsx";
import LoginPage from "./components/Pages/LoginPage/LoginPage.tsx";
import NotFoundPage from "./components/Pages/NotFoundPage/NotFoundPage.tsx";
import DashboardPage from "./components/Pages/DashboardPage/DashboardPage.tsx";
import PaymentsPage from "./components/Pages/PaymentsPage/PaymentsPage.tsx";

function App() {
  return (
    <BrowserRouter>
     <Routes>
       <Route path="/" element={<Layout />}>
         <Route index element={<Navigate to="/login" />} />
         <Route path="/login" element={<LoginPage/>}/>
         <Route path="/dashboard" element={<DashboardPage/>}/>
         <Route path="payments" element={<PaymentsPage/>}/>
       </Route>
       <Route path="*" element={<NotFoundPage/>} />
     </Routes>
    </BrowserRouter>
  )
}

export default App
