import axios from "../lib/axiosInstance";

export const getCommentsByPostId = (postId) =>
  axios.get(`/blogposts/${postId}/comments`).then((r) => r.data);

export const getCommentById = (postId, id) =>
  axios.get(`/blogposts/${postId}/comments/${id}`).then((r) => r.data);

export const createComment = (postId, comment) =>
  axios.post(`/blogposts/${postId}/comments`, comment).then((r) => r.data);
