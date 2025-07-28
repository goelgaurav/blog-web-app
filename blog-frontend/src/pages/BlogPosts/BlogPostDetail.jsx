import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import CommentCard from '../../components/CommentCard';
import axiosInstance from '../../lib/axiosInstance';

const BlogPostDetail = () => {
  const { id } = useParams();
  const [post, setPost]           = useState(null);
  const [comments, setComments]   = useState([]);
  const [newContent, setNewContent] = useState('');
  const [newAuthor, setNewAuthor] = useState('');
  const [loading, setLoading]     = useState(true);
  const [error, setError]         = useState(null);

  useEffect(() => {
    const load = async () => {
      try {
        const postRes     = await axiosInstance.get(`/blogposts/${id}`);
        setPost(postRes.data);

        const commentRes  = await axiosInstance.get(`/blogposts/${id}/comments`);
        setComments(commentRes.data);
      } catch (err) {
        setError(err);
      } finally {
        setLoading(false);
      }
    };
    load();
  }, [id]);

  const handleSubmit = async e => {
    e.preventDefault();
    try {
      const res = await axiosInstance.post(
        `/blogposts/${id}/comments`,
        { content: newContent, author: newAuthor }
      );
      setComments([res.data, ...comments]);
      setNewContent("");
      setNewAuthor("");
    } catch (err) {
      setError(err);
    }
  };

  if (loading) return <p className="p-4">Loading…</p>;
  if (error)   return <p className="p-4 text-red-600">Error: {error.message}</p>;

  return (
    <div className="p-6 max-w-3xl mx-auto">
      <h1 className="text-3xl font-bold mb-2">{post.title}</h1>
      <p className="text-sm text-gray-500 mb-4">
        {new Date(post.createdAt).toLocaleString()} — {post.author || "Anonymous"}
      </p>
      <div className="prose mb-8">
        {/* assuming your API returns HTML or markdown-rendered */}
        <div dangerouslySetInnerHTML={{ __html: post.content }} />
      </div>

      <section className="mb-6">
        <h2 className="text-2xl font-semibold mb-3">Comments</h2>
        {comments.length === 0
          ? <p>No comments yet.</p>
          : comments.map(c => <CommentCard key={c.id} comment={c} />)}
      </section>

      <section>
        <h3 className="text-xl font-semibold mb-2">Add a Comment</h3>
        <form onSubmit={handleSubmit} className="space-y-4">
          <textarea
            className="w-full border rounded p-2"
            placeholder="Your comment"
            value={newContent}
            onChange={e => setNewContent(e.target.value)}
            required
          />
          <input
            type="text"
            className="w-full border rounded p-2"
            placeholder="Your name (optional)"
            value={newAuthor}
            onChange={e => setNewAuthor(e.target.value)}
          />
          <button
            type="submit"
            className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
          >
            Post Comment
          </button>
        </form>
      </section>
    </div>
  );
};

export default BlogPostDetail;
