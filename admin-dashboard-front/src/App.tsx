import {BrowserRouter, Route, Routes} from "react-router-dom";
import Layout from "./components/Pages/Layout/Layout.tsx";
import LoginPage from "./components/Pages/LoginPage/LoginPage.tsx";
import Dashboard from "./components/Pages/Dashboard.tsx";
import NotFound from "./components/Pages/NotFound.tsx";

function App() {
  return (
    <BrowserRouter>
     <Routes>
       <Route path="/" element={<Layout />}>
         <Route index element={<LoginPage/>}/>
         <Route path="/dashboard" element={<Dashboard/>}/>
         <Route path="*" element={<NotFound/>} />
       </Route>
     </Routes>
    </BrowserRouter>
  )
}

export default App
