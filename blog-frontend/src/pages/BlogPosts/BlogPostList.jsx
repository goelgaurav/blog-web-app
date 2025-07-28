import { useEffect, useRef, useState } from 'react';
import BlogPostCard from '../../components/BlogPostCard';
import BlogPostSkeleton from '../../components/BlogPostSkeleton';
import axiosInstance from '../../lib/axiosInstance';
//import {getBlogPosts} from '../../api/blogPostsApi';

const BlogPostList = () => {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [displayCount, setDisplayCount]   = useState(5);
  const [loadingMore, setLoadingMore]     = useState(false);
  const loaderRef = useRef(null);

  useEffect(() => {
    axiosInstance.get('/blogposts')
      .then((res) => { 
        if(import.meta.env.DEV) console.log('Fetched blog posts:', res.data);
        if (Array.isArray(res.data)) {
            setPosts(res.data);
        } else {
          throw new Error('Expected array but got: ' + typeof res.data);
        }
      })
      .catch((err) => {
        console.error('Failed to fetch blog posts:', err);
        setError(err);
      })
        .finally(() => setLoading(false));
  }, []);

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

 const displayedPosts = posts.slice(0, displayCount);
 
 return (
    <div className="max-w-4xl mx-auto px-4">
        <h1 className="text-3xl font-extrabold my-6">All Blog Posts</h1>

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