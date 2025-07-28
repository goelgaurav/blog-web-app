export default function CommentCard({ comment }) {

    let date = '';
    if (comment.postedAt) {
        const d = new Date(comment.postedAt);
        date = isNaN(d.getTime())
            ? ''
            : d.toLocaleString();
    }

    return (
        <div className="border p-4 rounded mb-2 bg-gray-50">
            <p className="text-sm text-gray-700">{comment.content}</p>
            <div className="text-xs text-gray-500 mt-1">
                By <span className="font-medium">{comment.author || 'Anonymous'}</span> at{' '}
                {date && ` — ${date}`}
            </div>
        </div>
    );
}