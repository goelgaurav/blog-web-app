import { BrowserRouter, Link, Route, Routes } from 'react-router-dom';
import './App.css';
import ErrorBoundary from './components/ErrorBoundary';
import BlogPostList from './pages/BlogPosts/BlogPostList';
function App() {

  return (
    <BrowserRouter>
      <nav className="p-4 bg-gray-800 text-white">
        <Link to="/" className="mr-4">Posts</Link>
      </nav>
      <ErrorBoundary>
      <Routes>
        <Route path="/" element={<BlogPostList />}/>
      </Routes>
      </ErrorBoundary>
    </BrowserRouter>
  );
}

export default App;
