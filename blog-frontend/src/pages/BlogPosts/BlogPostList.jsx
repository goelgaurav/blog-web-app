// src/pages/BlogPosts/BlogPostList.jsx
import { useEffect, useState } from 'react';
import BlogPostCard from '../../components/BlogPostCard';
import axiosInstance from '../../lib/axiosInstance';

const BlogPostList = () => {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

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

  if (loading) return <p className="p-4">Loading...</p>;
  
  if (error) {
    return <div>Error fetching blog posts: {error.message}</div>;
  }

  return (
    <div className="max-w-4xl mx-auto px-4">
        <h1 className="text-3xl font-extrabold my-6">All Blog Posts</h1>

     {posts.length === 0
       ? <p className="text-gray-600">No blog posts found.</p>
       : posts.map((post) => <BlogPostCard key={post.id} post={post}/>)
     }
     
    </div>
  );
};

export default BlogPostList;