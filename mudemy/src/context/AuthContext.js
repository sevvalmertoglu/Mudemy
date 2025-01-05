import { createContext, useState, useContext } from "react";
import AuthService from '../services/AuthService'; 

const AuthContext = createContext();

export const useAuth = () => useContext(AuthContext);

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  const login = async (email, password) => {
    try {
      const userData = await AuthService.login(email, password); 
      //setUser({ name: userData.name }); 
    } catch (error) {
      console.error("Login failed", error);
      throw error;
    }
  };

  const logout = () => setUser(null);

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
