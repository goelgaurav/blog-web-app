import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { getBlogPosts } from '../../api/blogPostsApi';
import BlogPostCard from '../../components/BlogPostCard';
const BlogPostList = () => {
    const [posts, setPosts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [sortBy, setSortBy] = useState('recent');

    useEffect(() => {
        setLoading(true);
        getBlogPosts()
            .then((data) => {
                if (import.meta.env.DEV) console.log('Fetched blog posts:', data);
                if (Array.isArray(data)) {
                    if(data.length > 0) {
                        setPosts(data);
                    }
                    else {
                        toast.info('You have reached the end of blog posts.');
                    }
                } else {
                    throw new Error('Expected array but got: ' + typeof data);
                }
            })
            .catch((err) => {
                console.error('Failed to fetch blog posts:', err);
                toast.error('Failed to fetch blog posts');
                setError(err);
            })
            .finally(() => setLoading(false));
    }, []);

    if (loading) return <p className="p-4">Loading...</p>;

    if (error) {
        return <div>Error fetching blog posts: {error.message}</div>;
    }


    return (
        <div className="max-w-4xl mx-auto px-4">
            <h1 className="text-3xl font-extrabold my-6">All Blog Posts</h1>
            <div className="flex space-x-3 mb-6">
                <button
                    onClick={() => setSortBy('recent')}
                    className={`px-4 py-2 rounded-full transition 
                            ${sortBy === 'recent'
                            ? 'bg-blue-600 text-white'
                            : 'bg-gray-200 text-gray-700'}
                            `}
                >
                    Most Recent
                </button>
                <button
                    onClick={() => setSortBy('comments')}
                    className={`px-4 py-2 rounded-full transition
                            ${sortBy === 'comments'
                            ? 'bg-blue-600 text-white'
                            : 'bg-gray-200 text-gray-700'}
                        `}
                >
                    Most Commented
                </button>
            </div>

            {posts.length === 0
                ? <p>No blog posts found.</p>
                : posts.map(p => <BlogPostCard key={p.id} post={p} />)}
        </div>
    );
};

export default BlogPostList;
