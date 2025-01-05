import axios from 'axios';

const API_URL = 'https://localhost:5001/api/Order/';

const OrderService = {
  async createOrder(courseId) {
    try {
      const response = await axios.post(`${API_URL}`, { courseId });
      return response.data;
    } catch (error) {
      console.error('Error creating order:', error);
      throw new Error('An error occurred while creating the order.');
    }
  },
};

export default OrderService;
