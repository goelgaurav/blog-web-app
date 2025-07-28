import { Link } from 'react-router-dom';
const BlogPostCard = ({ post }) => {
    return (
        <Link to={`/blogposts/${post.id}`}>
            <article className="border border-gray-200 rounded-lg p-6 mb-6 bg-white hover:shadow-lg transition-shadow">
                <h2 className="text-xl font-semibold mb-2">{post.title}</h2>
                <time className="block text-sm text-gray-500 mb-4">
                    {new Date(post.createdAt).toLocaleString()}
                </time>
                <p className="text-gray-700 mb-4">{post.description}</p>
                <p className="text-xs text-gray-400 italic">Author: {post.author || 'Anonymous'}</p>
            </article>
        </Link>
    );
};

export default BlogPostCard;
