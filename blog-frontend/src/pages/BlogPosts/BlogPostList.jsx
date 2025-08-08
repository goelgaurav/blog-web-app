import { useEffect, useRef, useState } from 'react';
import { toast } from 'react-toastify';
import { getBlogPosts } from '../../api/blogPostsApi';
import BlogPostCard from '../../components/BlogPostCard';
import BlogPostSkeleton from '../../components/BlogPostSkeleton';
const BlogPostList = () => {
    const [posts, setPosts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [displayCount, setDisplayCount] = useState(5);
    const [loadingMore, setLoadingMore] = useState(false);
    const loaderRef = useRef(null);
    const [sortBy, setSortBy] = useState('recent');
    const [searchTerm, setSearchTerm] = useState('');

    useEffect(() => {
        //debugger
        const term = searchTerm.trim();
        getBlogPosts(term ? { search: term } : {})
            .then((data) => {
                if (import.meta.env.DEV) console.log('Fetched blog posts:', data);
                if (Array.isArray(data)) {
                    setPosts(data);
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
    }, [searchTerm]);

    // infinite-scroll observer
    useEffect(() => {
        if (!loaderRef.current) return;
        const obs = new IntersectionObserver(entries => {
            if (entries[0].isIntersecting && displayCount < posts.length) {
                setLoadingMore(true);
                setTimeout(() => {
                    setDisplayCount(c => Math.min(c + 5, posts.length));
                    setLoadingMore(false);
                }, 500);
            }
        }, { threshold: 1 });
        obs.observe(loaderRef.current);
        return () => obs.disconnect();
    }, [loaderRef, displayCount, posts.length]);

    if (loading) return <p className="p-4">Loading...</p>;

    if (error) {
        return <div>Error fetching blog posts: {error.message}</div>;
    }

    const sortedPosts = [...posts].sort((a, b) => {
        if (sortBy === 'comments') {
            return (b.commentCount || 0) - (a.commentCount || 0);
        }
        return new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime();
    });
    const displayedPosts = sortedPosts.slice(0, displayCount);

    return (
        <div className="max-w-4xl mx-auto px-4">
            <h1 className="text-3xl font-extrabold my-6">All Blog Posts</h1>
            <input 
                className="mb-4 w-full px-3 py-2 border border-gray-300 rounded"
                type="text" 
                placeholder="Search blog posts title..." 
                value={searchTerm} 
                onChange={e => setSearchTerm(e.target.value)}/>
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

            {displayedPosts.length === 0
                ? <p>No blog posts found.</p>
                : displayedPosts.map(p => <BlogPostCard key={p.id} post={p} />)}

            {/* loader placeholder */}
            <div ref={loaderRef} className="h-1"></div>
            {loadingMore &&
                Array(3).fill(0).map((_, i) => <BlogPostSkeleton key={i} />)
            }
        </div>
    );
};

export default BlogPostList;
