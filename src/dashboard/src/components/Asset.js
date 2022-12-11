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
    loaded: false,
    email: false,
    slack: false,
    sms: false,
  });
  const [monitorActive, setMonitor] = useState({
    loaded: false,
    isRunning: false,
  });

  useEffect(() => {
    if (!monitorActive.loaded && open) setMonitorStatus();
    if (!notifications.loaded && open) setNotificationState();
  }, [open]);

  const deleteAsset = async () => {
    await fetch(`http://localhost:5005/api/v1/assets/${name}`, {
      method: "DELETE",
    });
    onAssetStateChange();
  };

  const setNotificationState = async () => {
    let notResult = await fetch(
      `http://localhost:5005/api/v1/notifications/${name}`,
      {
        method: "GET",
      }
    );
    let notification = await notResult.json();
    setNotifications({
      email: notification.Email,
      slack: notification.Slack,
      sms: notification.SMS,
    });
  };

  const setMonitorStatus = async () => {
    let monitors = await fetch(
      `http://localhost:5005/api/v1/monitors/${name}`,
      {
        method: "GET",
      }
    );
    var monitor = await monitors.text();
    setMonitor({ loaded: true, isRunning: monitor === "true" });
  };

  const changeMonitorState = async () => {
    let headers = new Headers();
    headers.set("Content-Type", "application/json");
    await fetch(
      `http://localhost:5005/api/v1/monitors/${
        monitorActive.isRunning ? "stop" : "start"
      }`,
      {
        method: "POST",
        headers: headers,
        body: JSON.stringify({ Name: name }),
      }
    );
    setMonitor({ loaded: true, isRunning: !monitorActive.isRunning });
  };

  const changeNotificationState = async (newNotifications) => {
    debugger;
    let headers = new Headers();
    headers.set("Content-Type", "application/json");
    await fetch(`http://localhost:5005/api/v1/notifications/${name}`, {
      method: "GET",
      headers: headers,
      body: JSON.stringify({
        Email: newNotifications.email,
        SMS: newNotifications.sms,
        Slack: newNotifications.slack,
      }),
    });
    setNotifications(notifications);
  };
  return (
    <>
      <Card className="mt-3">
        <Card.Body>
          <Row>
            <Col sm={10} className="d-flex align-items-center">
              <Badge bg={monitorActive.isRunning ? "success" : "secondary"}>
                {monitorActive.isRunning ? "Running" : "Stopped"}
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
                      changeNotificationState({
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
                      changeNotificationState({
                        ...notifications,
                        sms: e.target.value,
                      })
                    }
                  />
                  <span className="me-3"></span>
                  <InputGroup.Text>Slack</InputGroup.Text>
                  <InputGroup.Checkbox
                    value={notifications.slack}
                    onChange={(e) =>
                      changeNotificationState({
                        ...notifications,
                        slack: e.target.value,
                      })
                    }
                  />
                </InputGroup>
                <Button
                  variant={monitorActive.isRunning ? "secondary" : "success"}
                  onClick={() => changeMonitorState()}
                >
                  {monitorActive.isRunning ? "Stop" : "Start"} monitoring
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
