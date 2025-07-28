import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import { getBlogPostById } from '../../api/blogPostsApi'; // ← post helper
import { createComment, getCommentsByPostId } from '../../api/commentsApi'; // ← comment helper
import CommentCard from '../../components/CommentCard';

const BlogPostDetail = () => {
    const { id } = useParams();
    const [post, setPost] = useState(null);
    const [comments, setComments] = useState([]);
    const [newContent, setNewContent] = useState('');
    const [newAuthor, setNewAuthor] = useState('');
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        (async () => {
            try {
                const p = await getBlogPostById(id);
                setPost(p);

                const c = await getCommentsByPostId(id);
                setComments(c);
            } catch (err) {
                console.error(err);
                setError(err);
                toast.error('Failed to load post or comments');
            } finally {
                setLoading(false);
            }
        })();
    }, [id]);

    const handleSubmit = async e => {
        e.preventDefault();
        try {
            const created = await createComment(id, { content: newContent, author: newAuthor });
            setComments([created, ...comments]);
            setNewContent('');
            setNewAuthor('');
            toast.success('Comment posted!');
        } catch (err) {
            console.error(err);
            toast.error('Failed to post comment');
        }
    };

    if (loading) return <p className="p-4">Loading…</p>;
    if (error) return <p className="p-4 text-red-600">Error: {error.message}</p>;

    return (
        <div className="p-6 max-w-3xl mx-auto">
            <h1 className="text-3xl font-bold mb-2">{post.title}</h1>
            <p className="text-sm text-gray-500 mb-4">
                {new Date(post.postedAt).toLocaleString()} — {post.author || "Anonymous"}
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
