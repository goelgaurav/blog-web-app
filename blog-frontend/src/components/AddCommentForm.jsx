//@param {{ postId: string, onCommentAdded: () => void }} props

import { useState } from 'react';
import { toast } from 'react-toastify';

export default function AddCommentForm({ postId, onCommentAdded }) {
    const [author, setAuthor] = useState('');
    const [content, setContent] = useState('');
    const [submitting, setSubmitting] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setSubmitting(true);
        try {
            await import('../lib/axiosInstance')
                .then(m => m.default.post(`/blogposts/${postId}/comments`, { author, content }));
            toast.success('Comment added');
            setAuthor('');
            setContent('');
            onCommentAdded();
        } catch {
            toast.error('Failed to add comment');
        } finally {
            setSubmitting(false);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="mt-4">
            <h3 className="font-semibold mb-2">Add a Comment</h3>
            <input
                className="w-full mb-2 p-2 border rounded"
                placeholder="Your name (optional)"
                value={author}
                onChange={e => setAuthor(e.target.value)}
            />
            <textarea
                className="w-full mb-2 p-2 border rounded"
                placeholder="Your comment"
                value={content}
                required
                onChange={e => setContent(e.target.value)}
            />
            <button
                type="submit"
                disabled={submitting}
                className="px-4 py-2 bg-blue-600 text-white rounded disabled:opacity-50"
            >
                {submitting ? 'Submitting…' : 'Submit Comment'}
            </button>
        </form>
    );
}
