// @param {{ author?: string, content: string, createdAt: string }} props
export default function CommentCard({ author, content, createdAt }) {
  return (
    <div className="border p-4 rounded mb-2 bg-gray-50">
      <p className="text-sm text-gray-700">{content}</p>
      <div className="text-xs text-gray-500 mt-1">
        By <span className="font-medium">{author || 'Anonymous'}</span> at{' '}
        {new Date(createdAt).toLocaleString()}
      </div>
    </div>
  );
}