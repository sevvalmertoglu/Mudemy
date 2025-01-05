import { createContext, useState, useContext } from "react";
import AuthService from '../services/AuthService'; 

const AuthContext = createContext();

export const useAuth = () => useContext(AuthContext);

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  const login = async (email, password) => {
    try {
      const userData = await AuthService.login(email, password); 
      setUser({ name: userData.name }); 
    } catch (error) {
      console.error("Login failed", error);
      throw error;
    }
  };

  const logout = async () => {
    try {
      await AuthService.logout();
      setUser(null); 
    } catch (error) {
      console.error("Logout failed", error);
      throw error;
    }
  };

  const registerUser = async (userName, email, password) => {
    try {
      const userData = await AuthService.registerUser(userName, email, password); 
      setUser({ name: userData.name });
    } catch (error) {
      console.error("Register failed", error);
      throw error;
    }
  };

  return (
    <AuthContext.Provider value={{ user, login, logout, registerUser }}>
      {children}
    </AuthContext.Provider>
  );
};
