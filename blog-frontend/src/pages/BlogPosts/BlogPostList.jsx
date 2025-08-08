import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { getBlogPosts } from '../../api/blogPostsApi';
import BlogPostCard from '../../components/BlogPostCard';
const BlogPostList = () => {
    const [posts, setPosts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [searchTerm, setSearchTerm] = useState('');
    const [query, setQuery] = useState('');
    const [page, setPage] = useState(1);
    const pageSize = 5;
    const [sortBy, setSortBy] = useState('recent');
    const [hasMore, setHasMore] = useState(true);

    useEffect(() => {
        setLoading(true);
        //debugger
        const term = query.trim();
        getBlogPosts({ search: term , page, pageSize, sort: sortBy })
            .then((data) => {
                if (import.meta.env.DEV) console.log('Fetched blog posts:', data);
                if (Array.isArray(data)) {
                    if (data.length > 0) {
                        setPosts(data);
                        setHasMore(data.length === pageSize);
                    } else {
                        toast.info(`You've reached the end of blog posts.`);
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
    }, [query, page, sortBy]);

    if (loading) {
         return <p className="p-4">Loading...</p>;
    }

    if (error) {
        return <div>Error fetching blog posts: {error.message}</div>;
    }

    const handleSearchSubmit = (e) => {
        if (e.key === 'Enter') {
            e.preventDefault();
            setPage(1);
            setQuery(searchTerm); 
        }
    };

    const handleSearchBlur = () => {
        setPage(1);
        setQuery(searchTerm); 
    };


    return (
        <div className="max-w-4xl mx-auto px-4">
            <h1 className="text-3xl font-extrabold my-6">All Blog Posts</h1>
            <input 
                className="mb-4 w-full px-3 py-2 border border-gray-300 rounded"
                type="text" 
                placeholder="Search blog posts title..." 
                value={searchTerm} 
                onChange={e => setSearchTerm(e.target.value)}
                onKeyDown={handleSearchSubmit}
                onBlur={handleSearchBlur}/>

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

            <div className='flex justify-center gap-4 my-6'>
                <button
                    disabled={page <= 1}
                    onClick={() => setPage(page - 1)}>
                    <span className="px-4 py-2 bg-gray-200 text-gray-700 rounded disabled:opacity-50 hover:bg-gray-300">
                        Previous
                    </span>
                </button>
                <button
                    onClick={() => setPage(page + 1)}
                    disabled={!hasMore}
                    className="px-4 py-2 bg-gray-200 text-gray-700 rounded hover:bg-gray-300">
                    <span className="px-4 py-2 bg-gray-200 text-gray-700 rounded disabled:opacity-50 hover:bg-gray-300">
                        Next
                    </span>
                </button>
            </div>
        </div>
    );
};

export default BlogPostList;
