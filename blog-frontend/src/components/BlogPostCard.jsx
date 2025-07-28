const BlogPostCard = ({ post }) => {
  return (
    <div className="border p-4 rounded mb-4 shadow-sm bg-white">
      <h2 className="text-xl font-semibold">{post.title}</h2>
      <p className="text-sm text-gray-500">{new Date(post.createdAt).toLocaleString()}</p>
      <p className="mt-2 text-gray-700">{post.description}</p>
      <p className="text-xs text-gray-400 mt-1">Author: {post.author || "Anonymous"}</p>
    </div>
  );
};

export default BlogPostCard;
