import axios from "../lib/axiosInstance";

export const getBlogPosts = ({search}) => axios.get("/blogposts" , {params: {search}}).then((r) => r.data);

export const getBlogPostById = (id) =>
  axios.get(`/blogposts/${id}`).then((r) => r.data);

export const createBlogPost = (post) =>
  axios.post("/blogposts", post).then((r) => r.data);
