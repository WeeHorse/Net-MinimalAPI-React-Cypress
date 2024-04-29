import {createBrowserRouter, createRoutesFromElements, Route, RouterProvider} from "react-router-dom";
import React from 'react';
import ReactDOM from 'react-dom/client';
import App from "./App.jsx";
import About from "./pages/About.jsx";
import Shop from "./pages/Shop.jsx";
import './index.css';

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    return <RouterProvider router={
        createBrowserRouter(
            createRoutesFromElements(
                <Route element={<App/>}>
                    <Route path={'/'} element={<Shop/>}/>
                    <Route path={'/about'} element={<About/>}/>
                </Route>
            )
        )
    } />
  </React.StrictMode>,
)
