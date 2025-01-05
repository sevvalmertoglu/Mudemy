import axios from 'axios';

const API_URL = 'https://localhost:5001/api/Auth/';

const AuthService = {
  async login(email, password) {
    try {
        const response = await axios.post(`${API_URL}Login`, { email, password }, {
            withCredentials: true, // CORS 
          });          
      return response.data;
    } catch (error) {
      throw new Error('Kullanıcı adı veya parola hatalı!');
    }
  },

};

export default AuthService;
