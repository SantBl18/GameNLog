import { BrowserRouter } from 'react-router-dom';
import './globals.css'
import Games from './pages/Games';

function App() {

  return (
    <BrowserRouter>
      <Games/>
    </BrowserRouter>
  )
}

export default App
