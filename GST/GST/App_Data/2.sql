ALTER TABLE `site`
	ADD COLUMN `PaymentId` INT(10) NULL DEFAULT NULL COMMENT 'Template Id' AFTER `AllotmentId`;