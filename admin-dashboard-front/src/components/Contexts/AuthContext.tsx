import {createContext, type ReactNode, useContext, useState} from "react";

type Auth = {
    token: string | null;
    email: string;
};

type AuthContextType = {
    auth: Auth;
    setAuth: (auth: Auth) => void;
    logout: () => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
    const [auth, setAuthState] = useState<Auth>({
        token: localStorage.getItem("token"),
        email: localStorage.getItem("email") || "",
    });

    const setAuth = (newAuth: Auth) => {
        localStorage.setItem("token", newAuth.token || "");
        localStorage.setItem("email", newAuth.email);
        setAuthState(newAuth);
    };

    const logout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("email");
        setAuthState({ token: null, email: "" });
    };

    return (
        <AuthContext.Provider value={{ auth, setAuth, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) throw new Error("useAuth must be used within an AuthProvider");
    return context;
};