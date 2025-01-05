import axios from 'axios';
import { setToken, removeToken } from '../helpers/AuthHelper';

const API_URL = 'https://localhost:5001/api/';


const AuthService = {
  async login(email, password) {
    try {
        const response = await axios.post(`${API_URL}Auth/Login`, { email, password }, {
          withCredentials: true, // CORS 
          }); 

          setToken(response.data.data.accessToken, response.data.data.refreshToken);
      return response.data;
    } catch (error) {
      console.error('Login Error:', error.message); 
      throw new Error('Kullanıcı adı veya parola hatalı!');
    }
  },

  async logout() {
    try {
      const response = await axios.post(`${API_URL}Auth/Logout`, {}, {
        withCredentials: true,
      });
      removeToken();
      return response.data;
    } catch (error) {
      throw new Error('Logout işlemi başarısız!');
    }
  },

  async registerUser(userName, email, password) {
    try {
      const response = await axios.post(`${API_URL}User/Register`, { userName, email, password });
      return response.data;
    } catch (error) {
      throw new Error('Kayıt işlemi başarısız!');
    }
  },

};

export default AuthService;
