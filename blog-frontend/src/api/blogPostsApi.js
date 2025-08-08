import axios from "../lib/axiosInstance";

export const getBlogPosts = ({search, page, pageSize, sort = 'recent'}) => axios.get("/blogposts" , {params: {search , page, pageSize, sort}}).then((r) => r.data);

export const getBlogPostById = (id) =>
  axios.get(`/blogposts/${id}`).then((r) => r.data);

export const createBlogPost = (post) =>
  axios.post("/blogposts", post).then((r) => r.data);
