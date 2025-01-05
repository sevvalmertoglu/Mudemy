import React, { useState } from 'react';
import { useCart } from '../context/CartContext';

export default function Profile() {
  const { purchasedItems, userInfo, updateUserInfo } = useCart();

  const [selectedOrder, setSelectedOrder] = useState(null);
  const [editMode, setEditMode] = useState(false);
  // const [formData, setFormData] = useState(userInfo);

  const orders = purchasedItems.reduce((acc, item, index) => {
    const orderIndex = Math.floor(index / 3);
    if (!acc[orderIndex]) acc[orderIndex] = [];
    acc[orderIndex].push(item);
    return acc;
  }, []);

  const handleChange = (e) => {
    // setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // updateUserInfo(formData);
    setEditMode(false);
  };

  return (
    <div className="container mt-4" >
      <h2 style={{ color:"#EE4E34", fontSize: "45px", fontWeight: "bold" }}>Profile</h2>

      {/* User Info */}
      <div className="profile-info mb-4">
        {!editMode ? (
          <>
            <p><strong>Username:</strong>  </p>
            <p><strong>Email:</strong> </p>
            <button className="btn"  style={{
              backgroundColor: '#EE4E34',
              color: '#fff',
              padding: '10px 20px',
              borderRadius: '25px',
              cursor: 'pointer',
              width: '300px',
            }} onClick={() => setEditMode(true)}>Edit Profile</button>
          </>
        ) : (
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label className="form-label">Username</label>
              <input type="text" name="username" /* value={formData.username} */ onChange={handleChange} className="form-control" />
            </div>
            <div className="mb-3">
              <label className="form-label">Email</label>
              <input type="email" name="email" /* value={formData.email} */ onChange={handleChange} className="form-control" />
            </div>
            <div className="mb-3">
              <label className="form-label">Password</label>
              <input type="password" name="password" /* value={formData.password} */ onChange={handleChange} className="form-control" />
            </div>
            <div className="mb-3">
              <label className="form-label"> New Password</label>
              <input type="password" name="password" /* value={formData.password} */ onChange={handleChange} className="form-control" />
            </div>
            <div className="mb-3">
              <label className="form-label">Repeat new password</label>
              <input type="password" name="password" /* value={formData.password} */ onChange={handleChange} className="form-control" />
            </div>
            <button type="submit"  style={{
              backgroundColor: '#EE4E34',
              color: '#fff',
              padding: '10px 20px',
              borderRadius: '25px',
              cursor: 'pointer',
              width: '200px',
            }} className="btn btn-success">Save Changes</button>
          </form>
        )}
      </div>

      <h2>Your Orders</h2>
      {orders.length === 0 ? (
        <p>You have no orders yet.</p>
      ) : (
        <ul className="list-group">
          {orders.map((order, index) => (
            <li key={index} className="list-group-item list-group-item-action" onClick={() => setSelectedOrder(order)}>
              <strong>Order {index + 1}</strong>
            </li>
          ))}
        </ul>
      )}

      {selectedOrder && (
        <div
          className="modal show d-block"
          tabIndex="-1"
          role="dialog"
          style={{ backgroundColor: "rgba(0,0,0,0.5)" }}
        >
          <div className="modal-dialog">
            <div className="modal-content bg-white shadow-sm rounded">
              <div className="modal-header">
                <h5>Order Details</h5>
                <button className="btn-close" onClick={() => setSelectedOrder(null)}></button>
              </div>
              <div className="modal-body">
                <ul className="list-group">
                  {selectedOrder.map((item, idx) => (
                    <li key={idx} className="list-group-item">
                      <strong>{item.title}</strong> - ${item.price} x {item.quantity}
                    </li>
                  ))}
                </ul>
              </div>
              <div className="modal-footer">
                <button className="btn btn-secondary" onClick={() => setSelectedOrder(null)}>Close</button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
