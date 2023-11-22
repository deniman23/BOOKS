import { Home } from "./components/Home";
import Books from "./components/Books"

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/books',
    element: <Books />
  }
];

export default AppRoutes;
