import axios from "axios";

const API_URL = "https://localhost:5001/api";


export const fetchProduct = async () => {
  try {
    const response = await axios.get(`${API_URL}/Course`);
    if (response.data && response.data.data) {
      return response.data.data.map((course) => ({
        title: course.name,
        description: course.description,
        price: course.price,
        category: course.category,
      }));
    }
    return [];
  } catch (error) {
    console.error("Error fetching products:", error);
    throw error;
  }
};

export const fetchProductById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/Course/${id}`);
    if (response.data && response.data.data) {
      const course = response.data.data;
      return {
        title: course.name,
        description: course.description,
        price: course.price,
        category: course.category,
      };
    }
    return null;
  } catch (error) {
    console.error(`Error fetching product with id ${id}:`, error);
    throw error;
  }
};
