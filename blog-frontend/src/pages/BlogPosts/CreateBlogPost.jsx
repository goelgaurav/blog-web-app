import { yupResolver } from '@hookform/resolvers/yup';
import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import * as yup from 'yup';
import { createBlogPost } from '../../api/blogPostsApi';

const schema = yup.object().shape({
    title: yup.string().required('Title is required').max(150, 'Max 150 chars'),
    description: yup.string().optional().max(1000, 'Max 1000 chars'),
    content: yup.string().required('Content is required'),
    author: yup.string().optional(),
});

export default function CreateBlogPost() {
    const navigate = useNavigate();
    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting }
    } = useForm({
        resolver: yupResolver(schema)
    });

    const onSubmit = async (data) => {
        try {
            await createBlogPost(data);
            toast.success('Post Created Successfully');
            navigate('/');
        } catch (err) {
            console.error('Error creating post:', err);
            alert('Failed to save post');
        }
    };

    return (
        <div className="max-w-2xl mx-auto bg-white p-6 rounded-lg shadow-md">
            <h1 className="text-2xl font-bold mb-6">New Blog Post</h1>

            <form onSubmit={handleSubmit(onSubmit)} className="space-y-5">
                {/* TITLE */}
                <div>
                    <label className="block font-medium mb-1">Title *</label>
                    <input
                        type="text"
                        {...register('title')}
                        className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
                    />
                    {errors.title && (
                        <p className="text-red-600 text-sm mt-1">{errors.title.message}</p>
                    )}
                </div>

                {/* DESCRIPTION */}
                <div>
                    <label className="block font-medium mb-1">Description</label>
                    <textarea
                        {...register('description')}
                        rows={3}
                        className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
                    />
                    {errors.description && (
                        <p className="text-red-600 text-sm mt-1">{errors.description.message}</p>
                    )}
                </div>

                {/* CONTENT */}
                <div>
                    <label className="block font-medium mb-1">Content *</label>
                    <textarea
                        {...register('content')}
                        rows={6}
                        className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
                    />
                    {errors.content && (
                        <p className="text-red-600 text-sm mt-1">{errors.content.message}</p>
                    )}
                </div>

                {/* AUTHOR */}
                <div>
                    <label className="block font-medium mb-1">Author</label>
                    <input
                        type="text"
                        {...register('author')}
                        className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
                    />
                    {errors.author && (
                        <p className="text-red-600 text-sm mt-1">{errors.author.message}</p>
                    )}
                </div>

                {/* ACTIONS */}
                <div className="flex items-center justify-between pt-4">
                    <button
                        type="submit"
                        disabled={isSubmitting}
                        className="bg-blue-600 text-white px-5 py-2 rounded hover:bg-blue-700 disabled:opacity-50"
                    >
                        {isSubmitting ? 'Saving…' : 'Save Post'}
                    </button>
                    <button
                        type="button"
                        onClick={() => navigate('/')}
                        className="text-gray-600 underline"
                    >
                        Cancel
                    </button>
                </div>
            </form>
        </div>
    );
}
``