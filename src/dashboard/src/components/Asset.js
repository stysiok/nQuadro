import React, { useEffect, useState } from "react";

import {
  Row,
  Col,
  Button,
  Card,
  Collapse,
  InputGroup,
  Badge,
} from "react-bootstrap";

const Asset = ({ name, onAssetStateChange }) => {
  const [open, setOpen] = useState(false);
  const [notifications, setNotifications] = useState({
    email: false,
    slack: false,
    sms: false,
  });
  const [monitorActive, setMonitor] = useState(false);

  useEffect(() => {
    if (!open) return;
    let fetchData = async () => {
      let monitors = await fetch(
        `http://localhost:5005/api/v1/monitors/${name}`,
        {
          method: "GET",
        }
      );
      let notResult = await fetch(
        `http://localhost:5005/api/v1/notifications/${name}`,
        {
          method: "GET",
        }
      );
      let notification = await notResult.json();
      let monitor = await notResult.text();
      setNotifications({
        email: notification.email,
        slack: notification.Slack,
        sms: notification.Sms,
      });
      setMonitor(monitor === "true");
    };
    fetchData();
  }, [open]);

  const deleteAsset = async () => {
    await fetch(`http://localhost:5005/api/v1/assets/${name}`, {
      method: "DELETE",
    });
    onAssetStateChange();
  };

  const changeMonitorState = async () => {
    await fetch(
      `http://localhost:5005/api/v1/assets/${name}/${
        monitorActive ? "stop" : "start"
      }`,
      {
        method: "PUT",
      }
    );
    onAssetStateChange();
  };
  return (
    <>
      <Card className="mt-3">
        <Card.Body>
          <Row>
            <Col sm={10} className="d-flex align-items-center">
              <Badge bg={monitorActive ? "success" : "secondary"}>
                {monitorActive ? "Running" : "Stopped"}
              </Badge>
              <span className="ms-3">{name}</span>
            </Col>
            <Col sm={2}>
              <Button
                className={"ms-1 float-end"}
                onClick={() => setOpen(!open)}
                variant={open ? "warning" : "primary"}
              >
                {open ? "Close" : "Open"}
              </Button>
            </Col>
            <Collapse in={open}>
              <div>
                <InputGroup className="mb-3">
                  <span className="d-flex align-items-center me-3">
                    Notifications:
                  </span>
                  <InputGroup.Text>Email</InputGroup.Text>
                  <InputGroup.Checkbox
                    value={notifications.email}
                    onChange={(e) =>
                      setNotifications({
                        ...notifications,
                        email: e.target.value,
                      })
                    }
                  />
                  <span className="me-3"></span>
                  <InputGroup.Text>SMS</InputGroup.Text>
                  <InputGroup.Checkbox
                    value={notifications.sms}
                    onChange={(e) =>
                      setNotifications({
                        ...notifications,
                        email: e.target.value,
                      })
                    }
                  />
                  <span className="me-3"></span>
                  <InputGroup.Text>Slack</InputGroup.Text>
                  <InputGroup.Checkbox
                    value={notifications.slack}
                    onChange={(e) =>
                      setNotifications({
                        ...notifications,
                        email: e.target.value,
                      })
                    }
                  />
                </InputGroup>
                <Button
                  variant={monitorActive ? "secondary" : "success"}
                  onClick={() => changeMonitorState()}
                >
                  {monitorActive ? "Stop" : "Start"} monitoring
                </Button>
                <Button
                  variant="danger"
                  className={"float-end"}
                  onClick={() => deleteAsset()}
                >
                  Delete asset
                </Button>
              </div>
            </Collapse>
          </Row>
        </Card.Body>
      </Card>
    </>
  );
};

export default Asset;
