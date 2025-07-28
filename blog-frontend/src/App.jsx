import { BrowserRouter, Route, Routes } from 'react-router-dom';
import ErrorBoundary from './components/ErrorBoundary';
import Layout from './components/Layout';
import BlogPostList from './pages/BlogPosts/BlogPostList';
function App() {

  return (
    <BrowserRouter>
      <ErrorBoundary>
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" element={<BlogPostList />}/>
        </Route>
      </Routes>
      </ErrorBoundary>
    </BrowserRouter>
  );
}

export default App;
