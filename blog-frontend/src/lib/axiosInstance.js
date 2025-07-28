import axios from 'axios';
const axiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
});
console.log(import.meta.env.VITE_API_BASE_URL);

// Debug logs: request + response
axiosInstance.interceptors.request.use((request) => {
  if(import.meta.env.DEV) console.log("🔼 Request:", request);
  return request;
});

axiosInstance.interceptors.response.use((response) => {
  if(import.meta.env.DEV) console.log("🔽 Response:", response);
  return response;
}, (error) => {
  console.error("🔥 Axios Error:", error);
  return Promise.reject(error);
});


export default axiosInstance;