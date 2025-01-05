export const setToken = (accessToken, refreshToken) => {
  if (accessToken) {
    localStorage.setItem('accessToken', accessToken); 
  }
  if (refreshToken) {
    localStorage.setItem('refreshToken', refreshToken); 
  }
};

export const getToken = () => {
  return {
    accessToken: localStorage.getItem('accessToken'), 
    //refreshToken: localStorage.getItem('refreshToken'), 
  };
};

export const removeToken = () => {
  localStorage.removeItem('accessToken');  
  localStorage.removeItem('refreshToken'); 
};
