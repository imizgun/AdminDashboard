import { createRoot } from 'react-dom/client'
import './index.scss'
import App from './App.tsx'
import {AuthProvider} from "./components/Contexts/AuthContext.tsx";

createRoot(document.getElementById('root')!).render(
    <AuthProvider>
        <App />
    </AuthProvider>
)
