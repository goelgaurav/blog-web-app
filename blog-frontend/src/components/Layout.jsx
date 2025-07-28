import { Link, Outlet } from 'react-router-dom';

export default function Layout() {
   return (
     <div className="min-h-screen bg-gray-50 text-gray-900 flex flex-col">
       <header className="bg-white shadow">
         <div className="max-w-4xl mx-auto px-4 py-4 flex justify-between items-center">
           <Link to="/" className="text-2xl font-bold">My Blog</Link>
           <Link to="/create" className="text-blue-600 hover:underline">New Post</Link>
         </div>
       </header>

       <main className="flex-1 max-w-4xl mx-auto px-4 py-8">
         <Outlet />
       </main>

       <footer className="bg-white border-t text-center py-4 text-sm text-gray-500">
         © {new Date().getFullYear()} My Blog
       </footer>
     </div>
   );
 }
