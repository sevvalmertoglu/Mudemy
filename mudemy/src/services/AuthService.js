import axios from 'axios';

const API_URL = 'https://localhost:5001/api/';

const AuthService = {
  async login(email, password) {
    try {
        const response = await axios.post(`${API_URL}Auth/Login`, { email, password }, {
            withCredentials: true, // CORS 
          });          
      return response.data;
    } catch (error) {
      throw new Error('Kullanıcı adı veya parola hatalı!');
    }
  },

  async logout() {
    try {
      const response = await axios.post(`${API_URL}Auth/Logout`, {}, {
        withCredentials: true,
      });
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
