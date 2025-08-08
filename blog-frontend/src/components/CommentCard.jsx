import { toast } from "react-toastify";
import { deleteComment } from "../api/commentsApi";

export default function CommentCard({ comment, postId, onDelete}) {

    let date = '';
    if (comment.postedAt) {
        const d = new Date(comment.postedAt);
        date = isNaN(d.getTime())
            ? ''
            : d.toLocaleString();
    }
    console.log(postId);

    const handleDelete = () => {
        if (!window.confirm('Are you sure you want to delete this comment?')) return;

        deleteComment(postId, comment.id)
            .then(() => {
                toast.success('Comment deleted');
                if (onDelete) {
                    onDelete(comment.id);
                }
                window.location.reload();
            })
            .catch((error) => {
                console.error('Failed to delete comment:', error);
                if (toast && toast.error) {
                    toast.error('Failed to delete comment');
                }
            });
    }
    return (
        <div className="border p-4 rounded mb-2 bg-gray-50">
            <p className="text-sm text-gray-700">{comment.content}</p>
            <div className="text-xs text-gray-500 mt-1">
                By <span className="font-medium">{comment.author || 'Anonymous'}</span> at{' '}
                {date && ` — ${date}`}
            <button
                className="float-right text-xs text-red-600 hover:underline"
                onClick={handleDelete}>
                Delete
            </button>
            </div>
        </div>
    );
}