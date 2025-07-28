import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import ErrorBoundary from './components/ErrorBoundary';
import Layout from './components/Layout';
import BlogPostDetail from './pages/BlogPosts/BlogPostDetail';
import BlogPostList from './pages/BlogPosts/BlogPostList';
import CreateBlogPost from './pages/BlogPosts/CreateBlogPost';

function App() {

  return (
    <BrowserRouter>
      <ErrorBoundary>
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" element={<BlogPostList />}/>
          <Route path="/create" element={<CreateBlogPost />}/>
          <Route path="/blogposts/:id" element={<BlogPostDetail />}/>

        </Route>
      </Routes>
      </ErrorBoundary>
      <ToastContainer position='bottom-right' autoClose={5000} hideProgressBar={false} closeOnClick pauseOnHover draggable pauseOnFocusLoss />
    </BrowserRouter>
  );
}

export default App;
