import axios from '../lib/axiosInstance';

export const blogPostsApi = {
  getAll:    () => axios.get('/blogposts').then(r => r.data),
  getById:  (id) => axios.get(`/blogposts/${id}`).then(r => r.data),
  create:   (post) => axios.post('/blogposts', post).then(r => r.data),
};