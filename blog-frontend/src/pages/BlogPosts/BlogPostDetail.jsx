import { useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import AddCommentForm from '../../components/AddCommentForm';
import CommentCard from '../../components/CommentCard';
import Spinner from '../../components/Spinner';
import axiosInstance from '../../lib/axiosInstance';

// Shows full post content and its comments, and a form to add new comments.

export default function BlogPostDetail() {
  const { id } = useParams();
  const [post, setPost] = useState(null);
  const [comments, setComments] = useState([]);
  const [loading, setLoading] = useState(true);

  const loadData = async () => {
    setLoading(true);
    try {
      const [{ data: p }, { data: c }] = await Promise.all([
        axiosInstance.get(`/blogposts/${id}`),
        axiosInstance.get(`/blogposts/${id}/comments`)
      ]);
      setPost(p);
      setComments(c);
    } catch {
      setPost(null);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { loadData(); }, [id]);

  if (loading) return <Spinner />;
  if (!post) return <p className="p-4">Post not found.</p>;

  return (
    <div className="p-6 max-w-3xl mx-auto">
      <Link to="/" className="text-blue-600 mb-4 inline-block">&larr; Back</Link>
      <h1 className="text-3xl font-bold mb-2">{post.title}</h1>
      <div className="text-sm text-gray-500 mb-4">
        {new Date(post.createdAt).toLocaleString()} by {post.author || 'Anonymous'}
      </div>
      <div className="prose mb-6" dangerouslySetInnerHTML={{ __html: post.content }} />
      <hr className="my-6" />
      <h2 className="text-2xl font-semibold mb-4">Comments</h2>
      {comments.length === 0 ? <p>No comments yet.</p> : comments.map(c =>
        <CommentCard key={c.id} {...c} />
      )}
      <AddCommentForm postId={id} onCommentAdded={loadData} />
    </div>
  );
}
