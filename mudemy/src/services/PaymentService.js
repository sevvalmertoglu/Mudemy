import axios from 'axios';

const API_URL = 'https://localhost:5001/api/Payment/';

const PaymentService = {
  async makePayment(cardDetails) {
    try {
      const response = await axios.post(`${API_URL}`, { cardDetails });
      return response.data; 
    } catch (error) {
      console.error('Payment failed:', error);
      throw error;
    }
  },
};

export default PaymentService;
