-- connect as user
CONN WORKSHOPUSR/development;

-- Print current active user
SHOW USER;
begin
  INSERT INTO VAN_WORKSHOP(WORKSHOPMESSAGE) VALUES ('WELCOME TO THE AWS LAMBDA WORKSHOP');
end;
/