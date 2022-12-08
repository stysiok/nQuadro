import React, { useState } from 'react';

import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';

function App() {
  const [addNewAssetFormActive, showAddNewAssetForm] = useState(false);
  console.log(addNewAssetFormActive)
  return (
    <Container className="p-3">
      <h1 className="header">nQuadro dashbaord</h1>
      <Button onClick={() => showAddNewAssetForm(true)}>Add asset</Button>
      {addNewAssetFormActive && <AddNewAssetForm onCancel={() => showAddNewAssetForm(false)} />}
    </Container>
  );
}

const AddNewAssetForm = ({ onCancel }) => {
  return (
    <>
      <Button onClick={onCancel} variant="light">Cancel</Button>      
    </>
  );
};


export default App;
