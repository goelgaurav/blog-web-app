import { Link } from 'react-router-dom';
const BlogPostCard = ({ post }) => {

    const desc = post.description
        ? post.description.length > 100
            ? post.description.slice(0, 100) + '…'
            : post.description
        : '';
    return (
        <Link to={`/blogposts/${post.id}`}>
            <article className="border border-gray-200 rounded-lg p-6 mb-6 bg-white hover:shadow-lg transition-shadow 
            w-full max-w-3xl mx-auto min-h-50 min-w-100 flex flex-col justify-between">
                <h2 className="text-xl font-semibold mb-1">{post.title}</h2>
                <time className="block text-sm text-gray-500 mb-4">
                    {new Date(post.createdAt).toLocaleString()}
                </time>
                <p className="text-gray-700 mb-4">{desc}</p>
                <div className="flex justify-between items-center">
                  <p className="text-xs text-gray-400 italic">
                    Author: {post.author || 'Anonymous'}
                  </p>
                  <p className="text-xs text-gray-500">
                    {post.commentCount ?? 0} comments
                  </p>
                </div>
            </article>
        </Link>
    );
};

export default BlogPostCard;