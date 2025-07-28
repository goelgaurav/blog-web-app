import { ErrorBoundary as ReactErrorBoundary } from 'react-error-boundary';

function ErrorFallback() {
  return (
    <div className="p-8 text-center">
      <h2 className="text-2xl font-semibold mb-2">Something went wrong.</h2>
      <p className="text-gray-600">Please try refreshing the page, or come back later.</p>
    </div>
  );
}

function logErrorToConsole(error, info) {
  // Log error details for debugging
  console.error('❌ Uncaught error in subtree:', error, info);
}

export default function ErrorBoundary({ children }) {
  return (
    <ReactErrorBoundary
      FallbackComponent={ErrorFallback}
      onError={logErrorToConsole}
    >
      {children}
    </ReactErrorBoundary>
  );
}